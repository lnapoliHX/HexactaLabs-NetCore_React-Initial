import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const ProducttypeForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Iniciales" name="initials" component={InputField} type="text" />
      <Field label="Descripción" name="description" component={InputField} type="text" />
      <Button className="producttype-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="producttype-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProducttypeForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "producttype",
  validate: Validator(schema),
  enableReinitialize: true
})(ProducttypeForm);
