import CreateUniversitySubjectForm from "../components/admin/createAcademyEntities/CreateUniversitySubjectForm";
import FormContainer from "../components/admin/createAcademyEntities/FormContainer";
import Header from "../components/auth/Header";

const CreateUniversitySubject = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie kierunku" />
      <CreateUniversitySubjectForm />
    </FormContainer>
  );
};

export default CreateUniversitySubject;
