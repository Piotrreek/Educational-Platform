import useAcademyEntities from "../hooks/useAcademyEntities";

import Container from "../components/didactic/didacticMaterials/Container";
import Filters from "../components/didactic/didacticMaterials/Filters";
import Materials from "../components/didactic/didacticMaterials/Materials";

const DidacticMaterials = () => {
  const { academyEntities, dispatch } = useAcademyEntities();

  return (
    <Container>
      <Filters dispatch={dispatch} filters={academyEntities} />
      <Materials filters={academyEntities} />
    </Container>
  );
};

export default DidacticMaterials;
