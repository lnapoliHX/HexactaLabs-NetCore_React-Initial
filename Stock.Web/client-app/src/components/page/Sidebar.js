import React from "react";
import PropTypes from "prop-types";
import { Nav, NavItem, Col } from "reactstrap";
import { NavLink as Link } from "react-router-dom";
import "./Sidebar.css";

const Sidebar = ({ menu }) => (
  <Col className="ui-sidebar"  sm={2}>    
    <h4 className="ui-sidebar-section-title">Navegación</h4>
    <Nav className="ui-sidebar-section-nav" vertical>
      {menu.map(({ to, icon, title }, i) => (
        <NavItem
          key={i}
          tag={() => (
            <Link to={to}>
              <i className={icon} />
              <p>{title}</p>
            </Link>
          )}
        />
      ))}
    </Nav>    
  </Col>
);

Sidebar.propTypes = {
  menu: PropTypes.arrayOf(
    PropTypes.shape({
      to: PropTypes.string.isRequired,
      icon: PropTypes.string,
      title: PropTypes.string
    })
  )
};

export default Sidebar;
