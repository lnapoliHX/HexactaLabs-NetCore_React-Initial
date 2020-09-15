import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import { create } from "../index";
import Form from "../../form/presentation";
import PropTypes from "prop-types";
import { Container, Row, Col } from "reactstrap";

const Create = ({ create: onSubmit, goBack: onCancel }) => {
  return (
    <Container fluid>
      <Row>
        <h2>Nuevo Tipo de Producto</h2>
      </Row>
      <Row>
        <Col>
          <Form onSubmit={onSubmit} handleCancel={onCancel} />
        </Col>
      </Row>
    </Container>
  );
};

Create.propTypes = {
  create: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

const mapDispatchToProps = {
  create,
  goBack
};

export default connect(
  null,
  mapDispatchToProps
)(Create);
