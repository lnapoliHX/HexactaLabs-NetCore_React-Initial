import React from "react";
import PropTypes from "prop-types";
import { Field, FormSection, reduxForm } from "redux-form";
import * as yup from "yup";
import Validator from "../helpers/YupValidator";
import renderField from "../modules/common/inputs/RenderField";
import selectable from "../modules/common/inputs/Selectable";

const options = [
  { value: "1", label: "Chocolate" },
  { value: "2", label: "Strawberry", disabled: true },
  { value: "3", label: "Vanilla" }
];

const AsyncValidationForm = ({ handleSubmit, pristine, reset, submitting }) => (
  <form onSubmit={handleSubmit}>
    <Field label="name" name="firstName" component={renderField} type="text" />
    <Field name="lastName" component={renderField} type="text" />
    <Field name="email" component={renderField} type="text" />
    <FormSection name="address">
      <Field label="calle" name="street" component={renderField} type="text" />
      <Field name="city" component={renderField} type="text" />
      <Field name="state" component={renderField} type="text" />
      <Field name="zip" component={renderField} type="text" />
    </FormSection>
    <Field
      name="prueba"
      options={options}
      component={selectable}
      type="select"
    />
    <div>
      <button type="submit" disabled={submitting}>
        Submit
      </button>
      <button type="button" disabled={pristine || submitting} onClick={reset}>
        Clear Values
      </button>
    </div>
  </form>
);

AsyncValidationForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  pristine: PropTypes.bool.isRequired,
  reset: PropTypes.func.isRequired,
  submitting: PropTypes.bool.isRequired
};

const schema = yup.object().shape({
  firstName: yup.string().required(),
  lastName: yup.string().required(),
  email: yup
    .string()
    .email()
    .required(),
  address: yup.object().shape({
    street: yup.string().required(),
    city: yup.string().required(),
    state: yup
      .string()
      .required()
      .min(2)
      .max(2),
    zip: yup.number().min(2)
  }),
  prueba: yup.string().required("Este campo es requerido")
});

export default reduxForm({
  form: "asyncValidation", // a unique identifier for this form
  validate: Validator(schema)
})(AsyncValidationForm);
