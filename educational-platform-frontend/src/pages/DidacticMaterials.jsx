import useAcademyEntities from "../hooks/useAcademyEntities";

import ContentContainer from "../components/didactic/common/ContentContainer";
import Filters from "../components/didactic/didacticMaterials/Filters";
import Materials from "../components/didactic/didacticMaterials/Materials";

const DidacticMaterials = () => {
  const { academyEntities, dispatch } = useAcademyEntities();

  return (
    <ContentContainer>
      <Filters dispatch={dispatch} filters={academyEntities} />
      <Materials filters={academyEntities} />
    </ContentContainer>
  );
};

export default DidacticMaterials;
