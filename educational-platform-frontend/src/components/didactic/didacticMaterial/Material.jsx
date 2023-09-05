import { useCallback, useEffect, useState } from "react";
import pdf from "../../../assets/pdf.svg";
import MaterialModal from "../didacticMaterials/MaterialModal";
import OpinionsSection from "../common/OpinionsSection";
import { useNavigate, useParams } from "react-router-dom";
import { getToken } from "../../../utils/jwtUtils";
import RatingsSection from "../common/RatingsSection";
import OriginSection from "./OriginSection";
import DescriptionSection from "../common/DescriptionSection";
import RateMaterial from "../common/RateMaterial";

const Material = () => {
  const [isModalOpened, setIsModalOpened] = useState(false);
  const [materialData, setMaterialData] = useState();
  const [rate, setRate] = useState(0);
  const routeData = useParams();
  const navigate = useNavigate();
  const id = routeData.id;

  const setOpinionsList = useCallback((opinions) => {
    setMaterialData((prev) => {
      const newState = { ...prev, opinions: opinions };

      return newState;
    });
  }, []);

  const handleRateChange = useCallback(
    (newRate, averageRating, lastRatings) => {
      setRate(newRate);
      setMaterialData((prev) => {
        const newState = {
          ...prev,
          averageRating: averageRating,
          lastRatings: lastRatings,
        };

        return newState;
      });
    },
    []
  );

  useEffect(() => {
    const fetchMaterial = async () => {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}didactic-material/${id}`,
          {
            method: "GET",
            credentials: "include",
            headers: {
              Authorization: `Bearer ${getToken()}`,
            },
          }
        );

        if (!response.ok) {
          navigate("/");
        }

        const data = await response.json();
        setMaterialData(data);
        setRate(data.usersRate ?? 0);
      } catch (_) {
        navigate("/");
      }
    };

    fetchMaterial();
  }, [id, navigate]);

  return (
    <div className="content">
      <div className="content__header">
        <h2 className="content__name">{materialData?.name.split(".")[0]}</h2>
        <div className="content__header-content">
          <div className="content__thumbnail">
            <img src={pdf} alt="thumbnail" />
          </div>
          <div className="content__actions">
            <a href={`${process.env.REACT_APP_BACKEND_URL}file/material/${id}`}>
              Pobierz
            </a>
            <button onClick={() => setIsModalOpened(true)}>Obejrzyj</button>
            <RateMaterial
              rate={rate}
              handleRateChange={handleRateChange}
              contentId={id}
              endPointPart="didactic-material"
            />
          </div>
        </div>
      </div>
      <div className="content__section content__description-section">
        <DescriptionSection description={materialData?.description} />
        <OriginSection
          author={materialData?.author}
          academy={materialData?.academy}
          faculty={materialData?.faculty}
          subject={materialData?.subject}
          course={materialData?.course}
        />
      </div>
      <RatingsSection
        ratings={materialData?.lastRatings ?? []}
        averageRating={materialData?.averageRating}
        noRatingsText={`Ten materiał nie posiada jeszcze żadnych ocen. Możesz go ocenić pod
        przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.`}
      />
      <OpinionsSection
        opinions={materialData?.opinions ?? []}
        setOpinionsList={setOpinionsList}
        noOpinionsText="Ten materiał nie posiada jeszcze żadnych opinii. Możesz dodać pod
        spodem opinię jeśli się zalogujesz."
      />
      {isModalOpened && (
        <MaterialModal
          onClose={() => setIsModalOpened(false)}
          files={[{ id: id }]}
          initIndex={0}
          contentType="material"
        />
      )}
    </div>
  );
};

export default Material;
