import React from "react";
import PropTypes from "prop-types";
import { Label, Input, FormFeedback, FormGroup } from "reactstrap";

const InputField = props => {
  const {
    input,
    label,
    placeholder,
    type,
    meta: { error, touched, pristine }
  } = props;

  return (
    <FormGroup>
      <Label htmlFor={input.name}>{label}</Label>
      <Input
        valid={touched && !error && !pristine}
        invalid={touched && !!error}
        {...input}
        id={input.name}
        placeholder={placeholder}
        type={type}
      />
      <FormFeedback>{error}</FormFeedback>
    </FormGroup>
  );
};

InputField.propTypes = {
  input: PropTypes.object,
  label: PropTypes.string.isRequired,
  type: PropTypes.string,
  meta: PropTypes.object,
  placeholder: PropTypes.string
};

export default InputField;
