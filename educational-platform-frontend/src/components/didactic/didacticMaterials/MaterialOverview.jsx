import classes from "./MaterialOverview.module.css";
import pdf from "../../../assets/pdf.svg";
import { useState } from "react";
import MaterialModal from "./MaterialModal";
import { Link } from "react-router-dom";
import { Rating, Typography } from "@mui/material";
import { StarRate } from "@mui/icons-material";

const MaterialOverview = ({ averageRating, author, name, id, materials }) => {
  const [isModalOpened, setIsModalOpened] = useState(false);

  return (
    <div className={classes.material}>
      <div className={classes.thumbnail}>
        <img src={pdf} alt="thumbnail" />
      </div>
      <div className={classes.material__description}>
        <p className={classes.material__name}>{name.split(".")[0]}</p>
        <p>
          <Typography component="legend">Średnia ocena</Typography>
          <Rating
            value={averageRating}
            precision={0.1}
            emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
            readOnly
            className={classes.rating}
          />
        </p>
        <div className={classes.material__actions}>
          <a href={`${process.env.REACT_APP_BACKEND_URL}file/material/${id}`}>
            Pobierz
          </a>
          <button
            className={classes["see-material"]}
            onClick={() => setIsModalOpened(true)}
          >
            Obejrzyj
          </button>
          <Link to={`/didactic-material/${id}`}>Szczegóły</Link>
        </div>
        <p>
          Autor:
          <span className={classes.material__author}>{author}</span>
        </p>
      </div>
      {isModalOpened && (
        <MaterialModal
          onClose={() => setIsModalOpened(false)}
          files={materials}
          initIndex={materials.findIndex((m) => m.id === id)}
        />
      )}
    </div>
  );
};

export default MaterialOverview;
