import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProductTypeView = props => {
  return (
    <Container fluid>
      <h3>Id:{props.productType.id}</h3>
      <Row>
        <Col lg="2">Iniciales</Col>
        <Col>{props.productType.initials}</Col>
      </Row>
      <Row>
        <Col lg="2">Descripción</Col>
        <Col>{props.productType.description}</Col>
      </Row>
      <div className="productType-view__button-row">
        <Button
          className="productType-form__button"
          color="primary"
          onClick={() => props.push(`/productType/update/${props.match.params.id}`)}
        >
          Editar
        </Button>
        <Button
          className="productType-form__button"
          color="danger"
          onClick={() =>
            props.push(`/productType/view/${props.match.params.id}/remove`)
          }
        >
          Eliminar
        </Button>
        <Button
          className="productType-form__button"
          color="default"
          onClick={() => props.push(`/productType`)}
        >
          Volver
        </Button>
      </div>
    </Container>
  );
};

ProductTypeView.propTypes = {
  productType: PropTypes.object.isRequired,
  push: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired
};

export default ProductTypeView;
