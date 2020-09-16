import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProductTypeView = props => {
  return (
    <Container fluid>
      <h1>{props.productType.initials}</h1>
      <Row>
        <Col lg="2">Descripción</Col>
        <Col>{props.productType.description}</Col>
      </Row>
      <div className="store-view__button-row">
        <Button className="store-form__button" color="primary" onClick={() => props.push(`/productType/update/${props.match.params.id}`)}>
          Editar
        </Button>
        <Button className="store-form__button" color="danger" onClick={() => props.push(`/productType/view/${props.match.params.id}/remove`)}>
          Eliminar
        </Button>
        <Button className="store-form__button" color="default" onClick={() => props.push(`/productType`)}>
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
