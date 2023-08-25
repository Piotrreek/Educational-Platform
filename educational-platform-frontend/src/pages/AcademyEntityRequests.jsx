import { useLoaderData } from "react-router-dom";
import { AcademyEntityTypes } from "../utils/academyEntityTypes";
import AcademyEntityRequestsByType from "../components/admin/academyEntityRequests/AcademyEntityRequestsByType";

const AcademyEntityRequests = () => {
  const loaderData = useLoaderData();
  const requestsGroupedByType = loaderData.requestsGroupedByType;

  const getHeadingByTypeName = (typeName) => {
    switch (typeName) {
      case AcademyEntityTypes.University:
        return "Prośby o dodanie uczelni";
      case AcademyEntityTypes.Faculty:
        return "Prośby o dodanie wydziału";
      case AcademyEntityTypes.UniversitySubject:
        return "Prośby o dodanie kierunku";
      default:
        return "Prośby o dodanie przedmiotu";
    }
  };

  return (
    <>
      {requestsGroupedByType.map((requestsWithType) => (
        <AcademyEntityRequestsByType
          key={requestsWithType.entityType}
          heading={getHeadingByTypeName(requestsWithType.entityType)}
          requests={requestsWithType.requests}
        />
      ))}
    </>
  );
};

export default AcademyEntityRequests;
