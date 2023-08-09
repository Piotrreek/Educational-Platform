import { createPortal } from "react-dom";
import Backdrop from "../../ui/Backdrop";
import classes from "./MaterialModal.module.css";
import { useState } from "react";
import arrowLeft from "../../../assets/arrowleft.svg";
import arrowRight from "../../../assets/arrowright.svg";
import exit from "../../../assets/exit.svg";

const MaterialModal = ({ onClose, files, initFile }) => {
  const [srcIndex, setSrcIndex] = useState(
    files.findIndex((s) => s.id === initFile.id)
  );
  const portalElement = document.getElementById("overlays");

  return (
    <>
      {createPortal(
        <Backdrop onClick={onClose}>
          <div className={classes.prev}>
            <img src={arrowLeft} alt="arrow-left" />
          </div>
          <div className={classes.next}>
            <img src={arrowRight} alt="arrow-right" />
          </div>
          <div className={classes.exit} onClick={onClose}>
            <img src={exit} alt="exit" />
          </div>
        </Backdrop>,
        portalElement
      )}
      {createPortal(
        <div className={classes.modal}>
          <div className={classes.content}>
            <iframe
              src="https://www.africau.edu/images/default/sample.pdf#zoom=100"
              title="material"
            />
          </div>
        </div>,
        portalElement
      )}
    </>
  );
};

export default MaterialModal;
