import { useState } from "react";
import { useLoaderData, useNavigate } from "react-router-dom";

import { AcademyEntityTypes } from "../utils/academyEntityTypes";
import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";

import AcademyEntityRequestsByType from "../components/admin/academyEntityRequests/AcademyEntityRequestsByType";
import { createUniversity } from "../actions/createUniversityAction";
import { createFaculty } from "../actions/createFacultyAction";
import { createUniversitySubject } from "../actions/createUniversitySubjectAction";
import { createUniversityCourse } from "../actions/createUniversityCourseAction";

export const ResolveAcademyRequestType = {
  Accept: "accept",
  Reject: "reject",
};

const AcademyEntityRequests = () => {
  const loaderData = useLoaderData();
  const navigate = useNavigate();
  const [error, setError] = useState("");
  const [requestsGroupedByType, setRequestsGroupedByType] = useState(
    loaderData.requestsGroupedByType
  );

  const resolveRequest = async (type, request) => {
    try {
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}academy/request/${request.id}/${type}`,
        {
          method: "POST",
          credentials: "include",
          headers: {
            Authorization: `Bearer ${getToken()}`,
          },
        }
      );

      if (response.status === 401) {
        navigate("/login");
      }

      const data = await response.json();

      if (response.status === 400) {
        setError(data.message);
        return;
      }

      if (response.ok) {
        if (type === ResolveAcademyRequestType.Accept) {
          switch (request.entityType) {
            case AcademyEntityTypes.University:
              await createUniversity(request.entityName);
              break;
            case AcademyEntityTypes.Faculty:
              await createFaculty(request.universityId, request.entityName);
              break;
            case AcademyEntityTypes.UniversitySubject:
              await createUniversitySubject(
                request.facultyId,
                request.subjectDegree,
                request.entityName
              );
              break;
            case AcademyEntityTypes.UniversityCourse:
              await createUniversityCourse(
                request.subjectId,
                request.courseSession,
                request.entityName
              );
              break;
            default:
              break;
          }
        }

        setRequestsGroupedByType(data);
      }
    } catch (_) {
      setError(BackendError);
    }
  };

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
          resolveRequest={resolveRequest}
        />
      ))}
    </>
  );
};

export default AcademyEntityRequests;
