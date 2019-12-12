import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

import Spinner from "../../../components/loading/spinner";
import LoginForm from "../presentational/Login";
import { login, signup } from "../index";

const LoginPage = ({ credentials, loading, errorMessage, login, signup }) => (
  <Spinner loading={loading}>
    <LoginForm
      errorMessage={errorMessage}
      onSubmit={values =>
        values.button === "login" ? login(values) : signup(values)
      }
      initialValues={credentials}
    />
  </Spinner>
);

LoginPage.propTypes = {
  credentials: PropTypes.object.isRequired,
  loading: PropTypes.bool.isRequired,
  errorMessage: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  signup: PropTypes.func.isRequired
};

const mapStateToProps = state => {
  return {
    credentials: { username: "", password: "" },
    loading: state.auth.loading,
    errorMessage: state.auth.errorMessage
  };
};

const mapDispatchToProps = { login, signup };

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LoginPage);
