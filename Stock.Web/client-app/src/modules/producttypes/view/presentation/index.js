import React from "react";
import { Container, Row, Col, Button } from "reactstrap";
import PropTypes from "prop-types";

const ProductTypeView = props => {
  return (
    <Container fluid>
      <div className="block-header">
        <h1>{props.producttype.initials}</h1>
      </div>
      <div className="info-box">
        <Row>
          <Col lg="2">Id</Col>
          <Col>{props.producttype.id}</Col>
        </Row>
        <Row>
          <Col lg="2">Descripción</Col>
          <Col>{props.producttype.description}</Col>
        </Row>

      </div>
      <div className="provider-view__button-row">
        <Button title="Editar" aria-label="Editar"
          className="provider-form__button"
          color="primary"
          onClick={() =>
            props.push(`/producttype/update/${props.match.params.id}`)
          }
        >
          <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 576 512" class="product-list__button-icon" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg"><path d="M402.6 83.2l90.2 90.2c3.8 3.8 3.8 10 0 13.8L274.4 405.6l-92.8 10.3c-12.4 1.4-22.9-9.1-21.5-21.5l10.3-92.8L388.8 83.2c3.8-3.8 10-3.8 13.8 0zm162-22.9l-48.8-48.8c-15.2-15.2-39.9-15.2-55.2 0l-35.4 35.4c-3.8 3.8-3.8 10 0 13.8l90.2 90.2c3.8 3.8 10 3.8 13.8 0l35.4-35.4c15.2-15.3 15.2-40 0-55.2zM384 346.2V448H64V128h229.8c3.2 0 6.2-1.3 8.5-3.5l40-40c7.6-7.6 2.2-20.5-8.5-20.5H48C21.5 64 0 85.5 0 112v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V306.2c0-10.7-12.9-16-20.5-8.5l-40 40c-2.2 2.3-3.5 5.3-3.5 8.5z"></path></svg>
        </Button>
        <Button title="Eliminar" aria-label="Eliminar"
          className="provider-form__button"
          color="danger"
          onClick={() =>
            props.push(`/producttype/view/${props.match.params.id}/remove`)
          }
        >
          <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 448 512" class="product-list__button-icon" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg"><path d="M432 32H312l-9.4-18.7A24 24 0 0 0 281.1 0H166.8a23.72 23.72 0 0 0-21.4 13.3L136 32H16A16 16 0 0 0 0 48v32a16 16 0 0 0 16 16h416a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16zM53.2 467a48 48 0 0 0 47.9 45h245.8a48 48 0 0 0 47.9-45L416 128H32z"></path></svg>
        </Button>
        <Button title="Volver" aria-label="Volver"
          className="provider-form__button btn-outline-secondary"
          color="default"
          onClick={() => props.push(`/producttype`)}
        >
          <svg xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" version="1.1" id="Layer_1" x="0px" y="0px" viewBox="0 0 470 474" width="20px" height="16px" xmlSpace="preserve">
            <path d="M384.834,180.699c-0.698,0-348.733,0-348.733,0l73.326-82.187c4.755-5.33,4.289-13.505-1.041-18.26    c-5.328-4.754-13.505-4.29-18.26,1.041l-82.582,92.56c-10.059,11.278-10.058,28.282,0.001,39.557l82.582,92.561    c2.556,2.865,6.097,4.323,9.654,4.323c3.064,0,6.139-1.083,8.606-3.282c5.33-4.755,5.795-12.93,1.041-18.26l-73.326-82.188    c0,0,348.034,0,348.733,0c55.858,0,101.3,45.444,101.3,101.3s-45.443,101.3-101.3,101.3h-61.58    c-7.143,0-12.933,5.791-12.933,12.933c0,7.142,5.79,12.933,12.933,12.933h61.58c70.12,0,127.166-57.046,127.166-127.166    C512,237.745,454.954,180.699,384.834,180.699z" />
          </svg>
        </Button>
      </div>
    </Container>
  );
};

ProductTypeView.propTypes = {
  producttype: PropTypes.object.isRequired,
  push: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired
};

export default ProductTypeView;
