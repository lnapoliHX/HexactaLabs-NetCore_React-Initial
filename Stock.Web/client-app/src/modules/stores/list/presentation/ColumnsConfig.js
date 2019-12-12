import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash, FaSearch } from "react-icons/fa";

const renderToolbar = ({ value }) => {
  let viewButton = (
    <Link className="store-list__button" to={`/store/view/${value}`}>
      <FaSearch className="store-list__button-icon" />
    </Link>
  );

  let editButton = (
    <Link className="store-list__button" to={`/store/update/${value}`}>
      <FaEdit className="store-list__button-icon" />
    </Link>
  );

  let removeButton = (
    <Link className="store-list__button" to={`/store/remove/${value}`}>
      <FaTrash className="store-list__button-icon" />
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
    <div
      style={{
        textAlign: "left",
        fontWeight: "bold"
      }}
    >
      {props.title}
    </div>
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
    Header: <HeaderComponent title="Direccion" />,
    accessor: "address",
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
