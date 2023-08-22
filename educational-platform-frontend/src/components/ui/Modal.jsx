import { createPortal } from "react-dom";

import Backdrop from "./Backdrop";

import classes from "./Modal.module.css";

const Modal = ({ children, backDropChildren, onClose, className }) => {
  const portalElement = document.getElementById("overlays");

  return (
    <>
      {createPortal(
        <Backdrop onClick={onClose}>{backDropChildren}</Backdrop>,
        portalElement
      )}
      {createPortal(
        <div className={`${classes.modal} ${!!className && className}`}>
          {children}
        </div>,
        portalElement
      )}
    </>
  );
};

export default Modal;
