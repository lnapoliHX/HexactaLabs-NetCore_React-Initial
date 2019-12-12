import React from "react";
import PropTypes from "prop-types";
import { Label } from "reactstrap";
import { DateTimePicker } from "react-widgets";
import Moment from "moment";
import momentLocalizer from "react-widgets-moment";

Moment.locale("es");
momentLocalizer();

const RenderFieldUpdate = props => {
  const {
    showTime,
    input: { name, value, onChange },
    label,
    meta: { error, touched }
  } = props;

  let className = "";
  if (touched && !error) {
    className = "border border-success";
  } else if (touched && error) {
    className = "border border-danger";
  }
  return (
    <div className="px-0 py-0">
      {label && <Label for={name}>{label}</Label>}
      <div className={className}>
        <DateTimePicker
          // {...input}
          onChange={onChange}
          format="DD MM YYYY"
          time={showTime}
          value={value ? new Date(value) : null}
        />
      </div>
      {touched && error ? <small className="text-danger">{error}</small> : null}
    </div>
  );
};

RenderFieldUpdate.propTypes = {
  showTime: PropTypes.object,
  label: PropTypes.string,
  input: PropTypes.object,
  meta: PropTypes.object
};

export default RenderFieldUpdate;
