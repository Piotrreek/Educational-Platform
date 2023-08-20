import Container from "../components/didactic/didacticMaterials/Container";
import Filters from "../components/didactic/didacticMaterials/Filters";
import Materials from "../components/didactic/didacticMaterials/Materials";
import useAcademyEntities from "../hooks/useAcademyEntities";

const DidacticMaterials = () => {
  const { materialFilters, dispatch } = useAcademyEntities();

  return (
    <Container>
      <Filters dispatch={dispatch} filters={materialFilters} />
      <Materials filters={materialFilters} />
    </Container>
  );
};

export default DidacticMaterials;
