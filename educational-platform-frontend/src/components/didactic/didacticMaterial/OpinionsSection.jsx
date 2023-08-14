import classes from "./Material.module.css";
import { Form, useActionData, useNavigation } from "react-router-dom";
import useInput from "../../../hooks/useInput";
import { notEmpty } from "../../../utils/validators";
import { useState, useEffect } from "react";
import useAuth from "../../../hooks/useAuth";
import Button from "../../ui/Button";

const OpinionsSection = ({ opinions, setOpinionsList }) => {
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
      <section className={classes.opinions}>
        <h2>Opinie</h2>
        {!!opinions.length ? (
          <>
            {opinions.map((opinion, id) => (
              <div className={classes.opinion} key={id}>
                <p className={classes.date}>
                  Data dodania:
                  <span>
                    {new Date(opinion.createdOn).toLocaleDateString("pl-PL")}
                  </span>
                </p>
                <p className={classes.author}>
                  Autor:
                  <span>{opinion.author}</span>
                </p>
                <p>{opinion.opinion}</p>
              </div>
            ))}
          </>
        ) : (
          <>
            Ten materiał nie posiada jeszcze żadnych opinii. Możesz dodać pod
            spodem opinię jeśli się zalogujesz.
          </>
        )}
      </section>

      {isLoggedIn && (
        <section className={classes["new-opinion"]}>
          <h2>Dodaj nową opinię</h2>
          <Form method="POST">
            <textarea
              name="opinion"
              onChange={onChange}
              onBlur={onBlur}
              value={value}
            ></textarea>
            <span className={classes.error}>{hasError && error}</span>
            {actionDataVisible && (
              <>
                <span className={classes.error}>
                  {!!submitError && submitError}
                </span>
                <span className={classes.success}>
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
