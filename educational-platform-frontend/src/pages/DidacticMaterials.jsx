import Container from "../components/didactic/didacticMaterials/Container";
import Filters from "../components/didactic/didacticMaterials/Filters";
import Materials from "../components/didactic/didacticMaterials/Materials";

const DidacticMaterials = () => {
  return (
    <Container>
      <Filters />
      <Materials />
    </Container>
  );
};

export default DidacticMaterials;
