import Container from "../components/didactic/didacticMaterials/Container";
import Filters from "../components/didactic/didacticMaterials/Filters";
import Materials from "../components/didactic/didacticMaterials/Materials";
import useMaterialFilters from "../hooks/useMaterialFilters";

const DidacticMaterials = () => {
  const { materialFilters, dispatch } = useMaterialFilters();

  return (
    <Container>
      <Filters dispatch={dispatch} filters={materialFilters} />
      <Materials filters={materialFilters} />
    </Container>
  );
};

export default DidacticMaterials;
