import React from "react";
import PropTypes from "prop-types";
import { Col } from "reactstrap";

const BodyContainer = ({ children }) => <Col>{children}</Col>;

BodyContainer.propTypes = {
  children: PropTypes.node
};

export default BodyContainer;
