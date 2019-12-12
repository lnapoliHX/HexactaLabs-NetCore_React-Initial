import React from "react";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from "reactstrap";
import { FaUserInjured } from "react-icons/fa";
import { NavLink as Link } from "react-router-dom";
import "./NavMenu.css";

export default class NavMenu extends React.Component {
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render() {
    return (
      <Navbar className="ui-header" expand="md">
        <NavbarBrand className="ui-header-brand">Hexacta Labs</NavbarBrand>
        <NavbarToggler onClick={this.toggle} />
        <Collapse isOpen={this.state.isOpen} navbar>
          <Nav className="ml-auto" navbar>
            <UncontrolledDropdown className="ui-header-options" nav inNavbar>
              <DropdownToggle caret>
                <FaUserInjured />
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem
                  tag={() => (
                    <Link className="dropdown-item" to="/logout">
                      Salir
                    </Link>
                  )}
                />
              </DropdownMenu>
            </UncontrolledDropdown>
          </Nav>
        </Collapse>
      </Navbar>
    );
  }
}
