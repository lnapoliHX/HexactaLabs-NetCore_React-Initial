import React from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";
import PropTypes from "prop-types";

const ElementRemove = ({ remove, goBack }) => {
  return (
    <Modal isOpen>
      <ModalHeader>Eliminar proveedor</ModalHeader>
      <ModalBody>¿Desea eliminar este proveedor?</ModalBody>
      <ModalFooter>
        <Button color="danger" onClick={remove}>
          Si
        </Button>
        <Button color="secondary" onClick={goBack}>
          No
        </Button>
      </ModalFooter>
    </Modal>
  );
};

ElementRemove.propTypes = {
  remove: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

ElementRemove.displayName = "ProviderRemove";

export default ElementRemove;
