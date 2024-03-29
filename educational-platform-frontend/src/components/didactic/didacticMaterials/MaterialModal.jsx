import { useEffect, useState } from "react";
import Modal from "../../ui/Modal";

import { ClipLoader } from "react-spinners";

import arrowLeft from "../../../assets/arrowleft.svg";
import arrowRight from "../../../assets/arrowright.svg";
import exit from "../../../assets/exit.svg";

import classes from "./MaterialModal.module.css";

const MaterialModal = ({ onClose, files, initIndex, contentType }) => {
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
          `${process.env.REACT_APP_BACKEND_URL}file/${contentType}/${files[srcIndex].id}`,
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
        URL.revokeObjectURL(src);
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

  const backdropContent = (
    <>
      {files.length > 1 && (
        <>
          <div className={classes.prev} onClick={prevHandler}>
            <img src={arrowLeft} alt="arrow-left" />
          </div>
          <div className={classes.next} onClick={nextHandler}>
            <img src={arrowRight} alt="arrow-right" />
          </div>
        </>
      )}
      <div className={classes.exit} onClick={onClose}>
        <img src={exit} alt="exit" />
      </div>
    </>
  );

  return (
    <Modal
      backDropChildren={backdropContent}
      className={(error || loading) && classes.modalErrorLoading}
      onClose={onClose}
    >
      <div>
        {!error && !loading && <iframe src={src} title="material" />}
        {error && <p>{error}</p>}
        <ClipLoader color="#fff" loading={loading} size={75} />
      </div>
    </Modal>
  );
};

export default MaterialModal;
