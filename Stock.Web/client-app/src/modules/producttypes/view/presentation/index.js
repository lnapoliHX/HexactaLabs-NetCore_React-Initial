import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const productypeView = props => {
  return (
    <Container fluid>
      <h1>{props.productype.Description}</h1>
      <Row>
        <Col lg="2">Descripcion</Col>
        <Col>{props.productype.Description}</Col>
      </Row>
      <Row>
        <Col lg="2">Iniciales</Col>
        <Col>{props.productype.Initials}</Col>
      </Row>
      <div className="productype-view__button-row">
        <Button
          className="productype-form__button"
          color="primary"
          onClick={() => props.push(`/productype/update/${props.match.params.id}`)}
        >
          Editar
        </Button>
        <Button
          className="productype-form__button"
          color="danger"
          onClick={() =>
            props.push(`/productype/view/${props.match.params.id}/remove`)
          }
        >
          Eliminar
        </Button>
        <Button
          className="productype-form__button"
          color="default"
          onClick={() => props.push(`/productype`)}
        >
          Volver
        </Button>
      </div>
    </Container>
  );
};

productypeView.propTypes = {
  productype: PropTypes.object.isRequired,
  push: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired
};

export default productypeView;
