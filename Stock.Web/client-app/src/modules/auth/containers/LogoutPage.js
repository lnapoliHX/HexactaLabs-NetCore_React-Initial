import React from "react";
import PropTypes from "prop-types";

import { connect } from "react-redux";
import { goBack } from "connected-react-router";

import { logout } from "../index";
import Logout from "../presentational/Logout";

const LogoutPage = ({ logout, goBack }) => (
  <Logout confirm={logout} cancel={goBack} />
);

LogoutPage.propTypes = {
  logout: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

const mapStateToProps = state => {
  return {
    loading: state.auth.loading
  };
};

const mapDispatchToProps = { logout, goBack };

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LogoutPage);
