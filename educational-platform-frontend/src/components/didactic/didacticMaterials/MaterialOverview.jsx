import classes from "./MaterialOverview.module.css";
import pdf from "../../../assets/pdf.svg";
import { useState } from "react";
import MaterialModal from "./MaterialModal";

const MaterialOverview = () => {
  const [isModalOpened, setIsModalOpened] = useState(false);

  return (
    <div className={classes.material}>
      <div className={classes.thumbnail}>
        <img src={pdf} alt="thumbnail" />
      </div>
      <div className={classes.material__description}>
        <p className={classes.material__name}>Całki oznaczone</p>
        <p>
          Średnia ocena: <span>4.65</span>
        </p>
        <div className={classes.material__actions}>
          <a href="#">Pobierz</a>
          <button
            className={classes["see-material"]}
            onClick={() => setIsModalOpened(true)}
          >
            Obejrzyj
          </button>
          <a href="#">Szczegóły</a>
        </div>
        <p>
          Autor:
          <span className={classes.material__author}>Jan Kowalski</span>
        </p>
      </div>
      {isModalOpened && (
        <MaterialModal
          onClose={() => setIsModalOpened(false)}
          files={[]}
          initFile={{ id: 1 }}
        />
      )}
    </div>
  );
};

export default MaterialOverview;
