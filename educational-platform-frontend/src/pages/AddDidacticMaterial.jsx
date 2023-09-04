import Header from "../components/auth/Header";
import FormContainer from "../components/forms/FormContainer";
import CreateDidacticMaterialForm from "../components/forms/CreateDidacticMaterialForm";

const AddDidacticMaterial = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie materiału dydaktycznego" />
      <CreateDidacticMaterialForm />
    </FormContainer>
  );
};

export default AddDidacticMaterial;
