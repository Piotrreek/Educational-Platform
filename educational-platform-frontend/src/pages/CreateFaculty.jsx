import CreateFacultyForm from "../components/admin/createAcademyEntities/CreateFacultyForm";
import FormContainer from "../components/admin/createAcademyEntities/FormContainer";
import Header from "../components/auth/Header";

const CreateFaculty = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie wydziału" />
      <CreateFacultyForm />
    </FormContainer>
  );
};

export default CreateFaculty;
