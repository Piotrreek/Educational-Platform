import classes from "./Materials.module.css";
import pdf from "../../../assets/pdf.svg";

const Materials = () => {
  return (
    <div className={classes.materials}>
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
            <a href="#">Obejrzyj</a>
            <a href="#">Szczegóły</a>
          </div>
          <p>
            Autor:
            <span className={classes.material__author}>Jan Kowalski</span>
          </p>
        </div>
      </div>
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
            <a href="#">Obejrzyj</a>
            <a href="#">Szczegóły</a>
          </div>
          <p>
            Autor:
            <span className={classes.material__author}>Jan Kowalski</span>
          </p>
        </div>
      </div>
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
            <a href="#">Obejrzyj</a>
            <a href="#">Szczegóły</a>
          </div>
          <p>
            Autor:
            <span className={classes.material__author}>Jan Kowalski</span>
          </p>
        </div>
      </div>
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
            <a href="#">Obejrzyj</a>
            <a href="#">Szczegóły</a>
          </div>
          <p>
            Autor:
            <span className={classes.material__author}>Jan Kowalski</span>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Materials;
