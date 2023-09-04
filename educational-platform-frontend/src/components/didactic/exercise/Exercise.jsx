import DescriptionSection from "../common/DescriptionSection";
import OpinionsSection from "../common/OpinionsSection";
import RateMaterial from "../common/RateMaterial";
import RatingsSection from "../common/RatingsSection";
import SolutionsSection from "./SolutionsSection";

import pdf from "../../../assets/pdf.svg";
import useAuth from "../../../hooks/useAuth";
import NewSolutionSection from "./NewSolutionSection";

const Exercise = () => {
  const { ctx } = useAuth();
  return (
    <div className="content">
      <div className="content__header">
        <h2 className="content__name">Ćwiczenie</h2>
        <div className="content__header-content">
          <div className="content__thumbnail">
            <img src={pdf} alt="thumbnail" />
          </div>
          <div className="content__actions">
            <a>Pobierz</a>
            <button>Obejrzyj</button>
            <RateMaterial
              rate={5}
              //   handleRateChange={handleRateChange}
              //   materialId={id}
            />
          </div>
        </div>
      </div>
      <div className="content__section content__description-section">
        <DescriptionSection description="Opis" />
      </div>
      <SolutionsSection solutions={[]} />
      {ctx.claims.isLoggedIn && <NewSolutionSection />}
      <RatingsSection
        ratings={[]}
        averageRating={4.5}
        noRatingsText={`To ćwiczenie nie posiada jeszcze żadnych ocen. Możesz je ocenić pod
        przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.`}
      />
      <OpinionsSection
        opinions={[]}
        setOpinionsList={() => {}}
        noOpinionsText="To ćwiczenie nie posiada jeszcze żadnych opinii. Możesz dodać pod spodem opinię jeśli się zalogujesz"
      />
    </div>
  );
};
export default Exercise;
