import React from "react";
import PropTypes from "prop-types";

const Logout = ({ confirm, cancel }) => (
  <div
    className="modal fade show"
    style={{ display: "block" }}
    tabIndex="-1"
    role="dialog"
  >
    <div className="modal-dialog" role="document">
      <div className="modal-content">
        <div className="modal-header">
          <h5 className="modal-title">Cerrar Sesión</h5>
        </div>
        <div className="modal-body">
          <p>¿Desea cerrar sesión?</p>
        </div>
        <div className="modal-footer">
          <button
            type="button"
            className="btn btn-primary"
            onClick={() => confirm()}
          >
            Sí
          </button>
          <button
            type="button"
            className="btn btn-secondary"
            onClick={() => cancel()}
          >
            No
          </button>
        </div>
      </div>
    </div>
  </div>
);

Logout.propTypes = {
  confirm: PropTypes.func.isRequired,
  cancel: PropTypes.func.isRequired
};

export default Logout;
