import { createPortal } from "react-dom";
import Backdrop from "../../ui/Backdrop";
import classes from "./MaterialModal.module.css";
import { useEffect, useState } from "react";
import arrowLeft from "../../../assets/arrowleft.svg";
import arrowRight from "../../../assets/arrowright.svg";
import exit from "../../../assets/exit.svg";
import { ClipLoader } from "react-spinners";

const MaterialModal = ({ onClose, files, initIndex }) => {
  const [srcIndex, setSrcIndex] = useState(initIndex);
  const [src, setSrc] = useState();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState();

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

  useEffect(() => {
    const abortController = new AbortController();
    const fetchMaterial = async () => {
      setLoading(true);
      setError();
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}file/material/${files[srcIndex].id}`,
          { signal: abortController.signal }
        );

        if (response.status === 404) {
          setLoading(false);
          setError("Nie znaleziono materiału");
          return;
        }

        if (!response.ok) {
          setLoading(false);
          setError("Wystąpił błąd na serwerze, spróbuj ponownie za chwilę.");
          return;
        }

        const data = await response.blob();
        const urlData = URL.createObjectURL(data);
        setSrc(urlData);
      } catch (e) {
        if (e.name === "AbortError") {
          return;
        }
      }
      setLoading(false);
    };

    fetchMaterial();

    return () => {
      abortController.abort();
    };
  }, [srcIndex, files]);

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
        <div
          className={`${classes.modal} ${
            (error || loading) && classes.modalErrorLoading
          }`}
        >
          <div className={classes.content}>
            {!error && !loading && <iframe src={src} title="material" />}
            {error && <p>{error}</p>}
            <ClipLoader color="#fff" loading={loading} size={75} />
          </div>
        </div>,
        portalElement
      )}
    </>
  );
};

export default MaterialModal;
