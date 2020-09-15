import React, { Component } from "react";
import PropType from "prop-types";
import { push } from "connected-react-router";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import { getProductTypeById } from "../../list/index";
import Presentation from "../presentation";
import Remove from "../../remove/container";

export class ProductTypeViewPage extends Component {
  render() {
    return (
      <React.Fragment>
        <Presentation productType={this.props.productType} {...this.props} />
        <Route path="/productType/view/:id/remove" component={Remove} />
      </React.Fragment>
    );
  }
}

ProductTypeViewPage.propTypes = {
  productType: PropType.object.isRequired
};

const mapStateToProps = (state, ownProps) => {
  return {
    productType: getProductTypeById(state, ownProps.match.params.id)
  };
};

const mapDispatchToProps = {
  push
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProductTypeViewPage);
