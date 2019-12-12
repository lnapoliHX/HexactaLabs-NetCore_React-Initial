import React from "react";
import { Input, Button } from "reactstrap";
import { MdSearch, MdCancel } from "react-icons/md";

import PropTypes from "prop-types";

const Search = props => {
  return (
    <React.Fragment>
      <div className="busqueda">
      <h4>Búsqueda</h4>
      <div className="flex-container">
			<div>
				<Input
					name="name"
					id="nameInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.name}
					placeholder="Nombre"
				/>
			</div>
			<div>
				<Input
					name="email"
					id="emailInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.email}
					placeholder="Email"
				/>
			</div>
			<div  className="lastCol">
				<Button color="primary" aria-label="Search" className="searchButton" onClick={props.submitFilter}>
					<MdSearch />
				</Button>
				<Button color="primary" aria-label="Clear" className="ml-3 clearButton" onClick={props.clearFilter}>
					<MdCancel />
				</Button>
			</div>
		</div>
      </div>
    </React.Fragment>
  );
};

Search.propTypes = {
  handleFilter: PropTypes.func.isRequired,
  submitFilter: PropTypes.func.isRequired,
  clearFilter: PropTypes.func.isRequired,
  filters: PropTypes.object.isRequired
};

export default Search;
