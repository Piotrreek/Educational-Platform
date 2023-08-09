import { createPortal } from "react-dom";
import Backdrop from "../../ui/Backdrop";
import classes from "./MaterialModal.module.css";
import { useState } from "react";
import arrowLeft from "../../../assets/arrowleft.svg";
import arrowRight from "../../../assets/arrowright.svg";
import exit from "../../../assets/exit.svg";

const MaterialModal = ({ onClose, files, initIndex }) => {
  const [srcIndex, setSrcIndex] = useState(initIndex);

  const prevHandler = () => {
    if (srcIndex === 0) {
      setSrcIndex(files.length - 1);
      return;
    }

    setSrcIndex((prev) => prev - 1);
  };

  const nextHandler = () => {
    if (srcIndex === files.length - 1) {
      setSrcIndex(0);
      return;
    }

    setSrcIndex((prev) => prev + 1);
  };

  const file = files.at(srcIndex);
  console.log(files);
  console.log(file);
  console.log(srcIndex);

  const portalElement = document.getElementById("overlays");

  return (
    <>
      {createPortal(
        <Backdrop onClick={onClose}>
          <div className={classes.prev} onClick={prevHandler}>
            <img src={arrowLeft} alt="arrow-left" />
          </div>
          <div className={classes.next} onClick={nextHandler}>
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
