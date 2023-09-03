import Header from "../components/auth/Header";
import FormContainer from "../components/didactic/FormContainer";
import CreateDidacticMaterialForm from "../components/didactic/CreateDidacticMaterialForm";

const AddDidacticMaterial = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie materiału dydaktycznego" />
      <CreateDidacticMaterialForm />
    </FormContainer>
  );
};

export default AddDidacticMaterial;
