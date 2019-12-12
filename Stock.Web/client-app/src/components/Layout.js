import React from "react";
import { connect } from "react-redux";
import { Container, Row } from "reactstrap";

import Sidebar from "./page/Sidebar";
import NavMenu from "./page/NavMenu";
// import Footer from './page/Footer';
import BodyContainer from "./page/BodyContainer";

const Layout = props => (
  <React.Fragment>
    <NavMenu />
    <Container fluid>
      <Row>
        <Sidebar {...props} />
        <BodyContainer {...props} />
      </Row>
    </Container>
  </React.Fragment>
);

const mapStateToProps = state => ({
  router: state.router
});

export default connect(mapStateToProps)(Layout);
