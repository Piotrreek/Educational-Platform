import { useState, useEffect } from "react";
import useAuth from "../../../hooks/useAuth";
import useInput from "../../../hooks/useInput";
import { useActionData, useNavigation } from "react-router-dom";

import Button from "../../ui/Button";
import { Form } from "react-router-dom";

import { notEmpty } from "../../../utils/validators";

import classes from "../didacticMaterial/Material.module.css";

const OpinionsSection = ({ opinions, setOpinionsList, noOpinionsText }) => {
  const [actionDataVisible, setActionDataVisible] = useState(false);
  const { ctx } = useAuth();
  const actionData = useActionData();
  const navigation = useNavigation();
  const { value, isValid, onBlur, onChange, error, hasError, reset } =
    useInput(notEmpty);

  useEffect(() => {
    reset();
    if (!!actionData?.isSuccess) {
      setOpinionsList(actionData.opinions);
    }
    setActionDataVisible(true);
    setTimeout(() => {
      setActionDataVisible(false);
    }, 3000);
  }, [actionData, reset, setOpinionsList]);

  const isSubmittingOpinion = navigation.state === "submitting";
  const submitError = actionData?.error;
  const submitIsSuccess = actionData?.isSuccess;
  const isLoggedIn = ctx.claims.isLoggedIn;

  return (
    <>
      <section className="content__section content__opinions-section">
        <h2>Opinie</h2>
        {!!opinions.length ? (
          <>
            {opinions.map((opinion, id) => (
              <div className="content__opinions-section__opinion" key={id}>
                <p className="content__opinions-section__date">
                  Data dodania:
                  <span>
                    {new Date(opinion.createdOn).toLocaleDateString("pl-PL")}
                  </span>
                </p>
                <p className="content__opinions-section__author">
                  Autor:
                  <span>{opinion.author}</span>
                </p>
                <p>{opinion.opinion}</p>
              </div>
            ))}
          </>
        ) : (
          <>{noOpinionsText}</>
        )}
      </section>

      {isLoggedIn && (
        <section className={`${classes["new-opinion"]} content__section`}>
          <h2>Dodaj nową opinię</h2>
          <Form method="POST">
            <textarea
              name="opinion"
              onChange={onChange}
              onBlur={onBlur}
              value={value}
            ></textarea>
            <span style={{ color: "coral" }} className={classes.error}>
              {hasError && error}
            </span>
            {actionDataVisible && (
              <>
                <span style={{ color: "coral" }} className={classes.error}>
                  {!!submitError && submitError}
                </span>
                <span
                  style={{ color: "greenyellow" }}
                  className={classes.success}
                >
                  {!!submitIsSuccess && "Pomyślnie dodano opinię"}
                </span>
              </>
            )}
            <Button disabled={!isValid || isSubmittingOpinion}>
              {isSubmittingOpinion ? "Dodaję" : "Dodaj"}
            </Button>
          </Form>
        </section>
      )}
    </>
  );
};

export default OpinionsSection;
