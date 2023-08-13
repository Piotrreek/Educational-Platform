import { useEffect, useState } from "react";
import pdf from "../../../assets/pdf.svg";
import classes from "./Material.module.css";
import MaterialModal from "../didacticMaterials/MaterialModal";
import AddOpinionForm from "./AddOpinionForm";
import { useNavigate, useParams } from "react-router-dom";
import { getToken } from "../../../utils/jwtUtils";

const Material = () => {
  const [isModalOpened, setIsModalOpened] = useState(false);
  const [materialData, setMaterialData] = useState();
  const routeData = useParams();

  const navigate = useNavigate();
  const id = routeData.id;

  useEffect(() => {
    const fetchMaterial = async () => {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}didactic-material/${id}`,
          {
            method: "GET",
            credentials: "include",
            headers: {
              Authorization: `Beare ${getToken()}`,
            },
          }
        );

        if (!response.ok) {
          navigate("/");
        }

        const data = await response.json();
        setMaterialData(data);
      } catch (_) {
        navigate("/");
      }
    };

    fetchMaterial();
  }, [id, navigate]);

  return (
    <div className={classes.material}>
      <div className={classes.header}>
        <h2 className={classes.material__name}>
          {materialData?.name.split(".")[0]}
        </h2>
        <div className={classes.header__content}>
          <div className={classes.thumbnail}>
            <img src={pdf} alt="thumbnail" />
          </div>
          <div className={classes.actions}>
            <a href={`${process.env.REACT_APP_BACKEND_URL}file/material/${id}`}>
              Pobierz
            </a>
            <button onClick={() => setIsModalOpened(true)}>Obejrzyj</button>
          </div>
        </div>
      </div>
      <div className={classes.description}>
        <section>
          <h2>Opis</h2>
          <p>
            {!!materialData?.description
              ? materialData.description
              : "Ten materiał nie posiada opisu"}
          </p>
        </section>
        <section className={classes.origin__info}>
          <h2>Pochodzenie materiału</h2>
          <p>
            Autor: <span>{materialData?.author}</span>
          </p>
          <p>
            Uczelnia: <span>{materialData?.academy}</span>
          </p>
          <p>
            Wydział: <span>{materialData?.faculty}</span>
          </p>
          <p>
            Kierunek: <span>{materialData?.subject}</span>
          </p>
          <p>
            Przedmiot: <span>{materialData?.course}</span>
          </p>
        </section>
      </div>
      <section className={classes.ratings}>
        <h2>Oceny</h2>
        {!!materialData?.lastRatings.length ? (
          <>
            <p>
              Średnia ocena w skali 1 - 5:
              <span>{materialData.averageRating.toFixed(1)}</span>
            </p>
            <p>
              Ostatnie oceny:
              {materialData.lastRatings.map((rating) => (
                <span>{rating.toFixed(1)}</span>
              ))}
            </p>
          </>
        ) : (
          <>
            Ten materiał nie posiada jeszcze żadnych ocen. Możesz go ocenić pod
            przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.
          </>
        )}
      </section>
      <section className={classes.opinions}>
        <h2>Opinie</h2>
        {!!materialData?.opinions.length ? (
          <>
            {materialData.opinions.map((opinion) => (
              <div className={classes.opinion}>
                <p className={classes.date}>
                  Data dodania: <span>{opinion.createdOn.toDateString()}</span>
                </p>
                <p className={classes.author}>
                  Autor: <span>{opinion.author}</span>
                </p>
                <p>{opinion.opinion}</p>
              </div>
            ))}
          </>
        ) : (
          <>
            Ten materiał nie posiada jeszcze żadnych opinii. Możesz dodać pod
            spodem opinię jeśli się zalogujesz.
          </>
        )}
      </section>
      <AddOpinionForm />
      {isModalOpened && (
        <MaterialModal
          onClose={() => setIsModalOpened(false)}
          files={[{ id: id }]}
          initIndex={0}
        />
      )}
    </div>
  );
};

export default Material;
