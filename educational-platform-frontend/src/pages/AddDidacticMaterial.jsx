import Header from "../components/auth/Header";
import DidacticMateriallFormContainer from "../components/didactic/DidacticMaterialFormContainer";
import CreateDidacticMaterialForm from "../components/didactic/CreateDidacticMaterialForm";

const AddDidacticMaterial = () => {
  return (
    <DidacticMateriallFormContainer>
      <Header heading="Tworzenie materiału dydaktycznego" />
      <CreateDidacticMaterialForm />
    </DidacticMateriallFormContainer>
  );
};

export default AddDidacticMaterial;
