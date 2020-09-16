import React from "react";
import columns from "./ColumnsConfig";
import { Container, Row, Col, Button } from "reactstrap";
import { FaPlus } from "react-icons/fa";
import ReactTable from "react-table";
import Search from "./ProductTypeSearch";

import PropTypes from "prop-types";

const Presentation = props => {
  return (
    <Container fluid>
      <Row className="my-1">
        <Col>
          <h1>Categorías de productos</h1>
        </Col>
      </Row>
      <Row>
        <Col>
        <Search
            handleFilter={props.onFilterChange}
            submitFilter={props.onFilterSubmit}
            clearFilter={props.onFilterReset}
            filters={props.filters}
          />
        </Col>
      </Row>
      <Row className="my-1">
        <Col>
          <Button className="productType__button" color="primary" aria-label="Agregar" onClick={() => props.push(props.urls.create)}>
            <FaPlus className="productType__button-icon" />
            AGREGAR
          </Button>
        </Col>
      </Row>
      <Row className="my-3">
        <Col>
          <ReactTable
            {...props}
            defaultPageSize={props.defaultPageSize}
            data={props.data}
            loading={props.dataLoading}
            columns={columns}
            className="-striped -highlight"
          />
        </Col>
      </Row>
    </Container>
  );
};

Presentation.propTypes = {
  data: PropTypes.array.isRequired,
  filters: PropTypes.object.isRequired,
  dataLoading: PropTypes.bool.isRequired,
  defaultPageSize: PropTypes.number,
  onFilterChange: PropTypes.func.isRequired,
  onFilterSubmit: PropTypes.func.isRequired,
  onFilterReset: PropTypes.func.isRequired,
  urls: PropTypes.shape({ create: PropTypes.string }),
  push: PropTypes.func.isRequired
};

export default Presentation;
