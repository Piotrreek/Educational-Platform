import { useCallback, useState } from "react";
import pdf from "../../../assets/pdf.svg";
import MaterialModal from "../didacticMaterials/MaterialModal";
import OpinionsSection from "../common/OpinionsSection";
import { useLoaderData, useParams } from "react-router-dom";
import RatingsSection from "../common/RatingsSection";
import OriginSection from "./OriginSection";
import DescriptionSection from "../common/DescriptionSection";
import RateMaterial from "../common/RateMaterial";

const Material = () => {
  const [isModalOpened, setIsModalOpened] = useState(false);
  const material = useLoaderData();
  const routeData = useParams();
  const id = routeData.id;

  const [ratingObject, setRatingObject] = useState({
    lastRatings: material.lastRatings,
    averageRating: material.averageRating,
  });

  const handleRateChange = useCallback((averageRating, lastRatings) => {
    setRatingObject({ lastRatings: lastRatings, averageRating: averageRating });
  }, []);

  return (
    <div className="content">
      <div className="content__header">
        <h2 className="content__name">{material.name.split(".")[0]}</h2>
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
              rate={material.usersRate ?? 0}
              handleRateChange={handleRateChange}
              contentId={id}
              endPointPart="didactic-material"
            />
          </div>
        </div>
      </div>
      <div className="content__section content__description-section">
        <DescriptionSection description={material.description} />
        <OriginSection
          author={material.author}
          academy={material.academy}
          faculty={material.faculty}
          subject={material.subject}
          course={material.course}
        />
      </div>
      <RatingsSection
        ratings={ratingObject.lastRatings}
        averageRating={ratingObject.averageRating}
        noRatingsText={`Ten materiał nie posiada jeszcze żadnych ocen. Możesz go ocenić pod
        przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.`}
      />
      <OpinionsSection
        opinionList={material.opinions}
        contentId={id}
        endpoint="didactic-material"
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
