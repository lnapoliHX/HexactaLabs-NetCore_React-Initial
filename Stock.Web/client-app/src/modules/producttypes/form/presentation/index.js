import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const producttypeForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Descripcion" name="Description" component={InputField} type="text" />
      <Field label="Iniciales" name="Initials" component={InputField} type="text" />
    
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

producttypeForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "producttype",
  validate: Validator(schema),
  enableReinitialize: true
})(producttypeForm);
