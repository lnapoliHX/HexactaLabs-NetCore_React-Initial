import React, { Component } from "react";
import { connect } from "react-redux";
import { getProviderById } from "../../list/index";
import Provider from "../presentation";
import Remove from "../../remove/container";
import { push } from "connected-react-router";
import { Route } from "react-router-dom";
import PropType from "prop-types";

export class ProvidersViewPage extends Component {
  render() {
    return (
      <React.Fragment>
        <Provider product={this.props.provider} {...this.props} />
        <Route path="/provider/view/:id/remove" component={Remove} />
      </React.Fragment>
    );
  }
}

ProvidersViewPage.propTypes = {
  provider: PropType.object.isRequired
};

const mapStateToProps = (state, ownProps) => {
  return {
    provider: getProviderById(state, ownProps.match.params.id)
  };
};

const mapDispatchToProps = {
  push
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProvidersViewPage);
