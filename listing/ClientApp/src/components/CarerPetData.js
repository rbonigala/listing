import React, { Component } from 'react';

export class CarerPetData extends Component {
    static displayName = CarerPetData.name;

  constructor(props) {
    super(props);
    this.state = { carerandpets: [], loading: true, isError: false };
  }

  componentDidMount() {
      this.populateCarerandpetsData();
  }

    static renderCarerandpetsTable(carerandpets) {
        return (
            <div>{carerandpets.map(cp =>
                <ul key={cp.gender} > {cp.gender}
                    {cp.names.map(n => <li>{n}</li>)}
                    </ul>
                )}</div>
            );
  }

  render() {
    
      let contents = this.state.isError
            ? <p>Service unavilable, please visit after sometime</p> : this.state.loading
          ? <p><em>Loading...</em></p>
          : CarerPetData.renderCarerandpetsTable(this.state.carerandpets);

    return (
      <div>
        <p>Listing of Carers and their pets</p>
        {contents}
      </div>
    );
  }

    async populateCarerandpetsData() {
        const response = await fetch('api/pet?petType=cat');
        console.log(`response status: ${response.status}`);
        if (response.status !== 200) {
            this.setState({ isError: true });
        }
        else {
            const data = await response.json();
            this.setState({ carerandpets: data, loading: false });
        }
    
  }
}
