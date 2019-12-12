import React from "react";
import { connect } from "react-redux";
import { load } from "../index";
import Home from "../presentation/Home";
import Spinner from "../../../components/loading/spinner";
import PropTypes from "prop-types";

class HomePage extends React.Component {
  componentDidMount() {
    //TODO: could use "useEffect"
    this.props.load();
  }

  render() {
    return (
      <Spinner loading={this.props.loading}>
        <Home />
      </Spinner>
    );
  }
}

const mapStateToProps = ({ home }) => {
  return {
    loading: home.loading
  };
};

const mapDispatchToProps = { load };

HomePage.propTypes = {
  loading: PropTypes.bool.isRequired,
  load: PropTypes.func.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(HomePage);
