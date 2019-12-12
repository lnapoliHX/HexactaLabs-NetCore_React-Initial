import React from "react";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash, FaSearch } from "react-icons/fa";

import PropTypes from "prop-types";

const renderToolbar = ({ value }) => {
  let viewButton = (
    <Link className="provider-list__button" to={`/provider/view/${value}`}>
      <FaSearch className="provider-list__button-icon" />
    </Link>
  );

  let editButton = (
    <Link className="provider-list__button" to={`/provider/update/${value}`}>
      <FaEdit className="provider-list__button-icon" />
    </Link>
  );

  let removeButton = (
    <Link className="provider-list__button" to={`/provider/remove/${value}`}>
      <FaTrash className="provider-list__button-icon" />
    </Link>
  );

  return (
    <span>
      {viewButton} {editButton} {removeButton}
    </span>
  );
};

const HeaderComponent = props => {
  return (
	<h2 
	className="tableHeading"
    >
      {props.title}
    </h2>
  );
};

HeaderComponent.displayName = "HeaderComponent";

const columns = [
  {
    Header: <HeaderComponent title="Nombre" />,
    accessor: "name",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Email" />,
    accessor: "email",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Telefono" />,
    accessor: "phone",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Acciones" />,
    accessor: "id",
    Cell: renderToolbar
  }
];

renderToolbar.propTypes = {
  value: PropTypes.string.isRequired
};

HeaderComponent.propTypes = {
  title: PropTypes.string.isRequired
};

export default columns;
