import React from "react";
import PropTypes from "prop-types";
import "./styles.css";

const Spinner = ({ loading, children }) => {
  if (loading) {
    return (
      <div className="spinner-border text-primary" role="status">
        <span className="sr-only">Loading...</span>
      </div>
    );
  }
  return children;
};

Spinner.propTypes = {
  loading: PropTypes.bool.isRequired,
  children: PropTypes.node.isRequired
};

export default Spinner;
