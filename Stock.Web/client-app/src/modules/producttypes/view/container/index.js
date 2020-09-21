import React, { Component } from "react";
import PropType from "prop-types";
import { push } from "connected-react-router";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import { getproducttypeById } from "../../list/index";
import Presentation from "../presentation";
import Remove from "../../remove/container";

export class productypeViewPage extends Component {
  render() {
    return (
      <React.Fragment>
        <Presentation productype={this.props.productype} {...this.props} />
        <Route path="/productype/view/:id/remove" component={Remove} />
      </React.Fragment>
    );
  }
}

productypeViewPage.propTypes = {
  productype: PropType.object.isRequired
};

const mapStateToProps = (state, ownProps) => {
  return {
    productype: getproducttypeById(state, ownProps.match.params.id)
  };
};

const mapDispatchToProps = {
  push
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(productypeViewPage);
