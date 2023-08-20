import CreateUniversityForm from "../components/admin/createAcademyEntities/CreateUniversityForm";
import FormContainer from "../components/admin/createAcademyEntities/FormContainer";
import Header from "../components/auth/Header";

const CreateUniversity = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie uczelni" />
      <CreateUniversityForm />
    </FormContainer>
  );
};
export default CreateUniversity;
