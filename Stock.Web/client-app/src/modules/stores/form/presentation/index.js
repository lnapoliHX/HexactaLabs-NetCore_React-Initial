import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const StoreForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Teléfono" name="phone" component={InputField} type="text" />
      <Field
        label="Dirección"
        name="address"
        component={InputField}
        type="text"
      />
      <Button className="store-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="store-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

StoreForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "provider",
  validate: Validator(schema),
  enableReinitialize: true
})(StoreForm);
