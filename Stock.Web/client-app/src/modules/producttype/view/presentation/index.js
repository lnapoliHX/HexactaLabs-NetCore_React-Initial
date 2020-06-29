import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProducttypeView = props => {
  return (
    <Container fluid>
      <h1>{props.producttype.initials}</h1>
      <Row>
        <Col lg="4">Descripción</Col>
        <Col>{props.producttype.description}</Col>
      </Row>
      <div className="producttype-view__button-row">
        <Button
          className="producttype-form__button"
          color="primary"
          onClick={() => props.push(`/producttype/update/${props.match.params.id}`)}
        >
          Editar
        </Button>
        <Button
          className="producttype-form__button"
          color="danger"
          onClick={() =>
            props.push(`/producttype/view/${props.match.params.id}/remove`)
          }
        >
          Eliminar
        </Button>
        <Button
          className="producttype-form__button"
          color="default"
          onClick={() => props.push(`/producttype`)}
        >
          Volver
        </Button>
      </div>
    </Container>
  );
};

ProducttypeView.propTypes = {
  producttype: PropTypes.object.isRequired,
  push: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired
};

export default ProducttypeView;
